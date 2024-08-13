import React from "react";
import { ScrapeSearchKeyword } from "../types/Scrape";

type Props = {
    keywords: ScrapeSearchKeyword[],
}

const ContentTable: React.FC<Props> = (props) => {
    if (!props.keywords || props.keywords.length < 1) {
        return (<p><em>fill the input then add</em></p>);
    }

    return (
        <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Index</th>
                    <th>Keyword</th>
                    <th>Domain</th>
                    <th>Search Engine</th>
                    <th>Occurence</th>
                </tr>
            </thead>
            <tbody>
                {props.keywords.map((keyword, i) =>
                    <tr key={i}>
                        <td>{i + 1}</td>
                        <td>{keyword.keyword}</td>
                        <td>{keyword.domain}</td>
                        <td>{keyword.searchEngine}</td>
                        <td>{keyword.result}</td>
                    </tr>
                )}
            </tbody>
        </table>);
};

export default ContentTable;