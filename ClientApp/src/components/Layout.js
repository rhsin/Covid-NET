// This layout component was modified from .NET Core's React template, using updated npm packages
// and converting to a functional component

import React, { createContext, useState, useEffect } from 'react';
import axios from 'axios';
import authService from './api-authorization/AuthorizeService'
import { url, listUrl, userUrl } from'./AppConstants';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

const defaultValue = {
  dailyCounts: [],
  countLists: [],
  users: [],
  loading: true
};

export const Context = createContext(defaultValue);

function Layout({ children }) {
  const [dailyCounts, setDailyCounts] = useState([]);
  const [countLists, setCountLists] = useState([]);
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [render, setRender] = useState(false);

  // The fetchData function has been configured to retrieve all neccessary
  // data from .NET API, with a request clean-up through Axios 
  useEffect(()=> {
    const cancelToken = axios.CancelToken;
    const source = cancelToken.source();

    fetchData(url, counts => setDailyCounts(counts));
    fetchData(listUrl, lists => setCountLists(lists));
    fetchData(userUrl, users => setUsers(users));
    
    return () => {
      source.cancel("Request Cancelled");
    }
  }, [render]);

  // Store acts as the global state, with functions to query more DailyCounts
  // Add/Remove DailyCount from CountList on button click, & function to re-render
  const store = {
    dailyCounts: dailyCounts,
    countLists: countLists,
    users: users,
    loading: loading,
    fetchCounts: (url) => fetchData(url, counts => setDailyCounts(counts)),
    handleCount: (url, action, listId, id) => handleCount(url, action, listId, id),
    setRender: () => setRender(!render) 
  };

  // SetState is a function parameter to pass setDailyCounts, setCountLists
  // or setUsers to update state with retrieved data
  const fetchData = async (url, setState) => {
    try {
      setLoading(true);
      const token = await authService.getAccessToken();
      const response = await axios.get(url, {
        headers: { 'Authorization': `Bearer ${token}` }
      });
      setState(response.data.data);
    } 
    catch (error) {
      console.log(error.message);
    } 
    finally {
      setLoading(false);
    }
  };

  // Action parameter takes string 'add' or 'remove' to create/delete
  // entry in join table (CountList & DailyCount)
  const handleCount = async (action, listId, id) => {
    try {
      setLoading(true);
      const token = await authService.getAccessToken();
      const response = await axios.post(
        listUrl + `DailyCount/${action}/${listId}/${id}`, {}, {
        headers: { 'Authorization': `Bearer ${token}` }
      });
      console.log(response.data);
    } 
    catch (error) {
      console.log(error.message);
    } 
    finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <Context.Provider value={store} >
        <NavMenu />
        <Container>
          {children}
        </Container>
      </Context.Provider>
    </div>
  );
}

export default Layout;