const scraperConfig = require('../../scrapper.config.json');
const CategoryScraper = require('./category');
const ProductScrapper = require('./product');
const colors = require('colors');

class Index {
    constructor(browser) {
        this.browser = browser;
        this.scraper = scraperConfig.simanSV;
        this.products = [];
        this.productsMetadata = [];
    }

    async initialize() {
        const page = await this.browser.newPage();
        await page.goto(this.scraper.url, { waitUntil: 'networkidle0' });
        await page.close();
        console.log(`${this.scraper.vendorId} scraper initialized`.green)
    }

    async scrapeCategories() {
        const categories = this.scraper.categories;
        for await (const category of categories) {
            console.log(`Scraping category: ${category.name}`.grey);
            const categoryScraper = new CategoryScraper(category.url, this.browser);
            await categoryScraper.initialize();
            const result = await categoryScraper.getProductsUrl(this.scraper.paginationFormat, category.name);
            this.products = [...this.products, ...result];
            await categoryScraper.close();
            console.log(`Done scraping category: ${category.name}`.green);
            console.log();
        }
    }

    async scrapeProducts() {
        if(this.products.length > 0) {
            let counter = 1;
            console.log(); //break line
            console.log(`Products to scrape: ${this.products.length}`.magenta); 
            for await (const product of this.products) {
                console.log(`#${counter} scraping url: ${product.url} category: ${product.category}`.grey)
                const productScraper = new ProductScrapper(this.browser);
                await productScraper.initialize();
                const data = await productScraper.getProductData(product);
                await productScraper.close();
                this.productsMetadata.push(data);
                console.log(`#${counter} Done scraping url: ${product.url} category: ${product.category}`.green);
                console.log();
                counter++;
            }
        }
    }
}

module.exports = Index;
