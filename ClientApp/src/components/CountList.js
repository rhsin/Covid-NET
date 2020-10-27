import React, { useState, useEffect } from 'react';
import axios from 'axios';
import authService from './api-authorization/AuthorizeService'
import ListForm from './ListForm';
import { listUrl, userUrl } from'./AppConstants';
import { Button } from 'reactstrap';

function CountList() {
  const [countLists, setCountLists] = useState([]);
  const [users, setUsers] = useState([]);
  const [user, setUser] = useState(null);
  const [listId, setListId] = useState(null);
  const [loading, setLoading] = useState(true);

  const dailyCountList = countLists.map(countList => countList.countListDailyCounts);

  useEffect(()=> {
    fetchLists();
    fetchUsers();
  }, []);

  const fetchLists = async () => {
    try {
      const token = await authService.getAccessToken();
      const response = await axios.get(listUrl, {
        headers: { 'Authorization': `Bearer ${token}` }
      })
      setCountLists(response.data.data);
    } catch (error) {
      console.log(error.message);
    }
    setLoading(false);
  };

  const fetchUsers = async () => {
    try {
      const token = await authService.getAccessToken();
      const response = await axios.get(userUrl, {
        headers: { 'Authorization': `Bearer ${token}` }
      })
      setUsers(response.data);
    } catch (error) {
      console.log(error.message);
    }
  };

  const handleClick = async (action, id) => {
    try {
      const token = await authService.getAccessToken();
      const response = await axios.post(
        listUrl + `DailyCount/${action}/${listId}/${id}`, {
        headers: { 'Authorization': `Bearer ${token}` }
      })
      console.log(response.data);
    } catch (error) {
      console.log(error.message);
    }
  };

  const selectUser = (id) => {
    const user = users.find(user => user.accountId == id);
    setUser(user);
  };

  return (
    <>
      {loading && <p><em>Loading...</em></p>}
      <div>
        <h1 id='covidTable'>COVID DailyCount Lists</h1>
        <p>This component demonstrates fetching data from the server.</p>
      </div>
      <ListForm
        users={users}
        user={user}
        selectUser={id => selectUser(id)}
        setListId={id => setListId(id)} 
      />
      {dailyCountList.map((dailyCounts, index) => dailyCounts.length > 0 &&
        <table 
          key={index} 
          className='table table-striped' 
          aria-labelledby='covidTable'
        >
          <thead>
            <tr>
              <th>Date</th>
              <th>County</th>
              <th>State</th>
              <th>Cases</th>
              <th>Deaths</th>
              <th>Remove</th>
            </tr>
          </thead>
          <tbody>
            {dailyCounts.map(item =>
              <tr key={item.dailyCount.id}>
                <td>{item.dailyCount.date}</td>
                <td>{item.dailyCount.county}</td>
                <td>{item.dailyCount.state}</td>
                <td>{item.dailyCount.cases}</td>
                <td>{item.dailyCount.deaths}</td>
                <td>
                  <Button
                    onClick={()=> handleClick('Remove', item.dailyCount.id)}
                  >
                    Remove
                  </Button>
                </td>
              </tr>
            )}
          </tbody>
        </table>
      )}
    </>
  );
}

export default CountList;


