const puppeteer = require('puppeteer')
const SimanScrapper = require('./scrappers/siman/scraper');

const scrapeData = async () => {;
    const browser = await puppeteer.launch();
    const scrapers = [
        SimanScrapper
    ]
    
    for await(const scraperClass of scrapers) {
        const scraper = new scraperClass(browser);
        await scraper.initialize();
        await scraper.scrapeCategories();
        await scraper.scrapeProducts();
    }
    
    await browser.close();
}

scrapeData()
.catch(error => console.error(error));