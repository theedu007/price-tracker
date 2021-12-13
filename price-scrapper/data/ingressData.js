const { MongoClient } = require('mongodb');

class IngressData {
    constructor(connectionString) {
        this.connectionString = connectionString;
    }

    async startMongoClient() {
        this.client = new MongoClient(this.connectionString, {
            
        });
        try {
            await this.client.connect();
            await this.client.db("admin").command({ ping: 1});
        } catch (error) {
            console.error("Error connecting to mongoDB instance");
            console.error(error);
            throw error;
        }
    }

    async IngressData(scraperResult, vendorId) {
        const database = this.client.db("priceTracker");
        const productsCollection = database.collection("products");
        const pricesCollection = database.collection("productPrices");

        const incomingProductsId = scraperResult.map(item => item.id);

        const existingProductsId = await productsCollection.find({
            productId: { $in: incomingProductsId }
        }).map(item => item.productId)
        .toArray();

        const newProducts = scraperResult.filter(item => !existingProductsId.includes(item.id));

        //Save new Products
        const dataToSave = newProducts.reduce((acc, item) => {
            const product = {
                vendorId: vendorId,
                name: item.name,
                productId: item.id,
                category: item.category,
                url: item.url
            };

            const itemPrice = item.productPrice;
            const productPrice = {
                price: itemPrice.price,
                date: itemPrice.date,
            }

            return [[product, productPrice], ...acc]
        }, []);

        for await(const pair of dataToSave) {

            const [product, price] = pair;

            const savedProduct = await productsCollection.insertOne(product);
            const productPrice = {
                ...price,
                productId: savedProduct.insertedId
            }

            await pricesCollection.insertOne(productPrice);
        }

        //Add new pricing to existing products
        const existingProducts = scraperResult.filter(item => existingProductsId.includes(item.id));
        if(existingProducts.lenght > 0) {
            const existingProductsCursor = await productsCollection.find({
                productId: { $in: existingProducts.map(item => item.id) }
            });
    
            for await (const item of existingProductsCursor) {
                const incomingProduct = scraperResult.find(x => x.id === item.productId);
    
                const price = {
                    productId: item._id,
                    ...incomingProduct.productPrice,
                }
    
                await pricesCollection.insertOne(price);
            }
        }
    }

    async CloseConnection() {
        await this.client.close();
    }
}

module.exports = IngressData;