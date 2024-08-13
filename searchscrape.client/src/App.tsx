import { useEffect, useState } from 'react';
import './App.css';
import { getScrapeSearchResult, getSearchEnignes } from './services/scrapeService';
import { ScrapeSearchKeyword } from './types/Scrape';
import ContentTable from './components/ContentTable';

interface SearchEngine {
    id: number,
    name: string,
    searchURL: string,
    searchKeywordQuery: string,
    isSelected: boolean,
}

function App() {
    const [searchEngines, setSearchEngines] = useState<SearchEngine[]>();
    const [keywords, setKeywords] = useState<ScrapeSearchKeyword[]>();
    const [keywordTyping, setKeywordTyping] = useState<string>('');
    const [domain, setDomain] = useState<string>('');

    useEffect(() => {
        getSearchEnignes().then(rs => {
            setSearchEngines(rs);
        });
    }, []);

    const onSelectSE = (e: any) => {
        const updatedState = searchEngines?.map(s => ({
            ...s,
            isSelected: s.id.toString() === e.target.value,
        }));

        setSearchEngines(updatedState);
    };

    const onDomainChange = (e: any) => {
        setDomain(e.target.value);
    };

    const keywordOnType = (e: any) => {
        setKeywordTyping(e.target.value);
    };

    const addKeyword = async () => {
        if (keywordTyping === '' || domain === '')
        {
            return;
        }

        const searchEngine = searchEngines?.find(s => s.isSelected);

        if (!searchEngine)
        {
            return;
        }

        const result = await getScrapeSearchResult(keywordTyping, searchEngine.id, domain);

        setKeywords([...(keywords ?? []), {
            keyword: keywordTyping,
            result,
            domain,
            searchEngine: searchEngine.name
        }]);

        setKeywordTyping('');
    };

    return (
        <div>
            <h1 id="tableLabel">Scraping</h1>
            <div>
                <label htmlFor='domain'> Search Domain: </label>
                <input id='domain' type="text" value={domain} onChange={onDomainChange}/>
            </div>

            <div>
                <label htmlFor='keyword'>By Keyword: </label>
                <input value={keywordTyping} onChange={keywordOnType} id='keyword' type="text" />
                <button onClick={addKeyword}>Add</button>
            </div>

            <div style={{ display: 'flex' }}>
                <p>Domain occurs in Search Engine</p>
                <div style={{marginLeft: '0.5rem'}}>
                    <select name="searchEngine" id="searchEngine" onChange={onSelectSE}>
                    <option>Select an search engine</option>
                        {
                            searchEngines?.map(s => (
                                <option selected={s.isSelected} id={s.id.toString()} value={s.id}>{s.name}</option>
                            ))
                        }
                    </select>
                </div>
            </div>

            <div style={{ marginTop: '1rem' }}>
                <ContentTable
                    keywords={keywords || []}
                />
            </div>
        </div>
    );
}

export default App;