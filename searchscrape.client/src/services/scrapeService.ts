import { EndPoints } from "../constants";

export async function getSearchEnignes() {
    const response = await fetch(EndPoints.getSearchEngineEndPoint);
    return await response.json();
};

export async function getScrapeSearchResult(keyword: string, searchEngineId: number, url: string) {
    const rawResponse = await fetch(EndPoints.getScrapeSearchResultEndPoint, {
        method: 'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            searchEngineId,
            keyword,
            url
        })
      });

      const result = await rawResponse.json();

      if (typeof(result) !== 'number') {
        return 0;
      }

      return result;
};