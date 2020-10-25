import React, { useState, useEffect } from 'react';
import axios from 'axios';
import authService from './api-authorization/AuthorizeService'
import SearchForm from './SearchForm';
import { url, listUrl } from'./AppConstants';
import { Button } from 'reactstrap';

function DailyCount() {
  const [dailyCounts, setDailyCounts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [listId, setListId] = useState(5);

  useEffect(()=> {
    fetchData(url);
  }, []);

  const fetchData = async (url) => {
    try {
      const token = await authService.getAccessToken();
      const response = await axios.get(url, {
        headers: { 'Authorization': `Bearer ${token}` }
      })
      setDailyCounts(response.data.data);
    } catch (error) {
      console.log(error.message);
    }
    setLoading(false);
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

  return (
    <>
      {loading && <p><em>Loading...</em></p>}
      <div>
        <h1 id='covidTable'>COVID Daily Counts</h1>
        <p>This component demonstrates fetching data from the server.</p>
      </div>
      <SearchForm
        setLoading={()=> setLoading(!loading)}
        fetchData={url => fetchData(url)} 
        setListId={id => setListId(id)}
      />
      <table className='table table-striped' aria-labelledby='covidTable'>
        <thead>
          <tr>
            <th>Date</th>
            <th>County</th>
            <th>State</th>
            <th>Cases</th>
            <th>Deaths</th>
            <th>Save</th>
            <th>Remove</th>
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
              <td>
                <Button
                  onClick={()=> handleClick('Add', dailyCount.id)}
                >
                  Save
                </Button>
              </td>
              <td>
                <Button
                  onClick={()=> handleClick('Remove', dailyCount.id)}
                >
                  Remove
                </Button>
              </td>
            </tr>
          )}
        </tbody>
      </table>
    </>
  );
}

export default DailyCount;


