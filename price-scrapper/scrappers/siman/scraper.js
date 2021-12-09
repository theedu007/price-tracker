const scraperConfig = require('../../scrapper.config.json');
const CategoryScraper = require('./category');
const ProductScrapper = require('./product');

class Index {
    constructor(browser) {
        this.browser = browser;
        this.scraper = scraperConfig.siman;
        this.products = [];
        this.productsMetadata = [];
    }

    async initialize() {
        const page = await this.browser.newPage();
        await page.goto(this.scraper.url, { waitUntil: 'networkidle0' });
        await page.close();
    }

    async scrapeCategories() {
        const categories = this.scraper.categories;
        for await (const category of categories) {
            const categoryScraper = new CategoryScraper(category.url, this.browser);
            await categoryScraper.initialize();
            const result = await categoryScraper.getProductsUrl(this.scraper.paginationFormat, category.name);
            this.products = [...this.products, ...result];
            await categoryScraper.close();
        }
    }

    async scrapeProducts() {
        if(this.products.length > 0) {
            for await (const product of this.products) {
                const productScraper = new ProductScrapper(this.browser);
                await productScraper.initialize();
                const data = await productScraper.getProductData(product);
                await productScraper.close();
                this.productsMetadata.push(data);
            }
        }

        const a = 1;
    }
}

module.exports = Index;