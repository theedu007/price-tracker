class CategoryScraper {
    
    constructor(url, browser) {
        this.url = url;
        this.browser = browser;
        this.page = undefined;
    }

    async initialize() {
        this.page = await this.browser.newPage();
    }

    async getProductsUrl(paginationformat, category) {
        let pageCounter = 1;
        let productsUrl = [];
        
        while(true) {
            const paginatedUrl = `${this.url}${paginationformat}${pageCounter}`;
            await this.page.goto(paginatedUrl, { waitUntil: 'networkidle2' });
            const container = await this.page.$('.vtex-search-result-3-x-searchResultContainer');

            if(container) {
                const hasError = await container.evaluate(elem => elem.innerText.includes('No se ha encontrado ningún producto'));
                if(hasError)
                    break;
            }
            const linkElements = await container.$$('.vtex-search-result-3-x-galleryItem a');
            for await (const linkNode of linkElements) {
                const url = await linkNode.evaluate(x => x.href);
                productsUrl.push({ category: category, url: url});   
            }
            pageCounter++;
        }   
        return productsUrl;
    }

    async close() {
        await this.page.close();
    }
}

module.exports = CategoryScraper;