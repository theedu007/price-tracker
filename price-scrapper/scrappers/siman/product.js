class ProductScrapper {

    constructor(browser) {
        this.browser = browser;
        this.page = undefined;
    }

    async initialize() {
        this.page = await this.browser.newPage();
    }

    async getProductData(product) {
        const { category, url } = product;
        let productId = undefined;
        let productPrice = undefined;
        let productName = undefined;

        await this.page.goto(url, { waitUntil: 'networkidle2', timeout: 50000 });

        const productIdMetatag = await this.page.$('meta[property="product:retailer_item_id"]');
        const productPriceMetatag = await this.page.$('meta[property="product:price:amount"]');
        const productNameMetatag = await this.page.$('.vtex-store-components-3-x-productNameContainer--quickview');
        
        if(productIdMetatag) {
            productId = await productIdMetatag.evaluate(node => node.getAttribute('content'));
        }
        if (productPriceMetatag) {
            productPrice = await productPriceMetatag.evaluate(node => node.getAttribute('content'));
        }
        if (productPriceMetatag) {
            productName = await productNameMetatag.evaluate(node => node.textContent);
        }

        if(productId && productPrice && productName) {
            const date = new Date();
            return {
                id: productId,
                productPrice: {
                    price: productPrice,
                    date: date
                },
                category: category,
                url: url,
                name: productName
            }
        } else {
            debugger;
        }
    }

    async close() {
        await this.page.close();
    }
}

module.exports = ProductScrapper;