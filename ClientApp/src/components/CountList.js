import React, { useState, useEffect } from 'react';
import axios from 'axios';
import authService from './api-authorization/AuthorizeService'
import { listUrl } from'./AppConstants';
import { Button } from 'reactstrap';

function CountList() {
  const [countLists, setCountLists] = useState([]);
  const [loading, setLoading] = useState(true);

  const dailyCountList = countLists.map(countList => countList.countListDailyCounts);

  useEffect(()=> {
    fetchData();
  }, []);

  const fetchData = async () => {
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

  const handleClick = async (action, id) => {
    try {
      const token = await authService.getAccessToken();
      const response = await axios.post(
        listUrl + `DailyCount/${action}/5/${id}`, {
        headers: { 'Authorization': `Bearer ${token}` }
      })
      console.log(response.data);
    } catch (error) {
      console.log(error.message);
    }
  };

  return (
    <>
      {loading && <p><em>Loading...</em></p>}
      <div>
        <h1 id='covidTable'>COVID DailyCount Lists</h1>
        <p>This component demonstrates fetching data from the server.</p>
      </div>
      {dailyCountList.map((dailyCounts, index) => 
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
                  <Button onClick={()=> handleClick('Remove', item.dailyCount.id)}>
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


