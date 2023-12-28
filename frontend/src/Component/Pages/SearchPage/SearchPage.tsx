import React, { ChangeEvent, SyntheticEvent, useState } from 'react'
import { CompanySearch } from '../../../company';
import CardList from '../../CardList/CardList';
import Hero from '../../Hero/Hero';
import Navbar from '../../Navbar/Navbar';
import ListPortfolio from '../../Portfolio/ListPortfolio/ListPortfolio';
import { searchCompains } from '../../api';
import Search from '../../Search/Search';

interface Props {}

const SearchPage = (props: Props) => {
    const [search, setSearch] = useState<string>("");
  const [searchResult, setSearchResult] = useState<CompanySearch[]>([]);
  const[serverError, setServerError] = useState<string>("");
  const [portfolioValues, setPortfolioValues] = useState<string[]>([]);

  const handleSearchSubmit = (e: ChangeEvent<HTMLInputElement>) =>{
    setSearch(e.target.value);
    console.log(e);
  };
  
  const onPortfolioCreate = (e: any) => {
    e.preventDefault();
    const exist = portfolioValues.find((value) => value === e.target[0].value);
    const updatedpPortfolio = [...portfolioValues, e.target[0].value];
    setPortfolioValues(updatedpPortfolio);
  }
  const onPortfolioDelete = (e: any) =>{
   e.preventDefailt();
   const removed = portfolioValues.filter((value) => {
    return value !== e.target[0].value;
   });
   setPortfolioValues(removed);
  }

  const onSearchSubmit = async (e: SyntheticEvent) =>{
    e.preventDefault();
    const result = await searchCompains(search);
    console.log({result});
    if(typeof result === "string"){
       setServerError(result);
    }
    else if(Array.isArray(result.data)){
        setSearchResult(result.data);
    }
  }
  return (
    <>
      <Search onSearchSubmit={onSearchSubmit} search={search} handleSearchSubmit={handleSearchSubmit}/>
      <ListPortfolio portfolioValues={portfolioValues} onPortfolioDelete={onPortfolioDelete}/>
      <CardList searchResult={searchResult} onPortfolioCreate={onPortfolioCreate}/>
      {serverError && <div>Unable to connect to API </div>}
      </>
  )
}

export default SearchPage