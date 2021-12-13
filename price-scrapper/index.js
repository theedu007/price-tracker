const puppeteer = require('puppeteer')
const SimanScrapper = require('./scrappers/siman/scraper');
const IngressData = require('./data/ingressData');

const scrapeData = async () => {;
    const browser = await puppeteer.launch({headless: true});
    const metadata = {};
    const scrapers = [
        SimanScrapper
    ];

    for await(const scraperClass of scrapers) {
        const scraper = new scraperClass(browser);
        await scraper.initialize();
        await scraper.scrapeCategories();
        await scraper.scrapeProducts();
        metadata[scraper.scraper.vendorId] = scraper.productsMetadata;
    }

    await browser.close();

    const keys = Object.keys(metadata);
    if(keys.length > 0) {
        const saveData = new IngressData('mongodb://127.0.0.1');
        await saveData.startMongoClient();

        for await(const key of keys) {
            const data = metadata[key];
            await saveData.IngressData(data, key);
        }
        await saveData.CloseConnection();
    }
}

scrapeData()
.catch(error => console.error(error));