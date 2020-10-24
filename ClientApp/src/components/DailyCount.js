import React, { useState, useEffect } from 'react';
import axios from 'axios';
import authService from './api-authorization/AuthorizeService'
import SearchForm from './SearchForm';
import { url } from'./AppConstants';

function DailyCount() {
  const [dailyCounts, setDailyCounts] = useState([]);
  const [loading, setLoading] = useState(true);

  const fetchData = async (url) => {
    const token = await authService.getAccessToken();
    const response = await axios.get(url, {
      headers: {'Authorization': `Bearer ${token}` }
    })
    .catch(error => console.log(error));
    setDailyCounts(response.data.data);
    setLoading(false);
  };

  useEffect(()=> {
    fetchData(url);
  }, []);

  return (
    <>
      {loading && <p><em>Loading...</em></p>}
      <div>
        <h1 id='covidTable' >COVID Daily Counts</h1>
        <p>This component demonstrates fetching data from the server.</p>
      </div>
      <SearchForm
      setLoading={()=> setLoading(!loading)}
      fetchData={(url)=> fetchData(url)} 
      />
      <table className='table table-striped' aria-labelledby='covidTable'>
        <thead>
          <tr>
            <th>Date</th>
            <th>County</th>
            <th>State</th>
            <th>Cases</th>
            <th>Deaths</th>
          </tr>
        </thead>
        <tbody>
          {dailyCounts.map(dailyCount =>
            <tr key={dailyCount.id}>
              <td>{dailyCount.date}</td>
              <td>{dailyCount.county}</td>
              <td>{dailyCount.state}</td>
              <td>{dailyCount.cases}</td>
              <td>{dailyCount.deaths}</td>
            </tr>
          )}
        </tbody>
      </table>
    </>
  );
}

export default DailyCount;


