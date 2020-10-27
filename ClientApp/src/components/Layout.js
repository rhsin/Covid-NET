import React, { createContext, useState, useEffect } from 'react';
import axios from 'axios';
import authService from './api-authorization/AuthorizeService'
import { url, listUrl, userUrl } from'./AppConstants';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export const Context = createContext();

function Layout({ children }) {
  const [dailyCounts, setDailyCounts] = useState([]);
  const [countLists, setCountLists] = useState([]);
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(()=> {
    fetchData(url, counts => setDailyCounts(counts));
    fetchData(listUrl, lists => setCountLists(lists));
    fetchData(userUrl, users => setUsers(users));
  }, [loading]);

  const fetchData = async (url, setState) => {
    try {
      const token = await authService.getAccessToken();
      const response = await axios.get(url, {
        headers: { 'Authorization': `Bearer ${token}` }
      })
      setState(response.data.data);
    } catch (error) {
      console.log(error.message);
    }
    setLoading(false);
  };

  const handleClick = async (action, listId, id) => {
    try {
      const token = await authService.getAccessToken();
      const response = await axios.post(
        listUrl + `DailyCount/${action}/${listId}/${id}`, {}, {
        headers: { 'Authorization': `Bearer ${token}` }
      })
      console.log(response.data);
    } catch (error) {
      console.log(error.message);
    }
    setLoading(!loading);
  };

  const store = {
    dailyCounts: dailyCounts,
    countLists: countLists,
    users: users,
    loading: loading,
    fetchCounts: (url) => fetchData(url, counts => setDailyCounts(counts)),
    handleClick: (url, action, listId, id) => handleClick(url, action, listId, id) 
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