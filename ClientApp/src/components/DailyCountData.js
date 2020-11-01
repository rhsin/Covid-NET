// This component fetches the daily & average case count for the selected county, state, month.

import React, { useState, useEffect } from 'react';
import axios from 'axios';
import DataForm from './DataForm';
import { urlData } from'./AppConstants';

function DailyCountData() {
  const [dailyCounts, setDailyCounts] = useState([]);
  const [average, setAverage] = useState(null);
  const [loading, setLoading] = useState(true);
  const [render, setRender] = useState(false);

  useEffect(()=> {
    fetchData('San Diego', 'CA', 9);
  }, [render]);

  const fetchData = async (county, state, month) => {
    try {
      setLoading(true);
      const response = await axios.get(urlData(county, state, month));
      setDailyCounts(response.data.data.data);
      setAverage(response.data.data.value[0].Average);
    } 
    catch (error) {
      console.log(error.message);
    } 
    finally {
      setLoading(false);
    }
  };

  return (
    <>
      {loading && <p><em>Loading...</em></p>}
      <div>
        <h1 className='covidTable'>COVID Daily Counts Data</h1>
        <p>This component demonstrates fetching data from the server.</p>
      </div>
      <DataForm 
        fetchCounts={(county, state, month) => fetchData(county, state, month)} 
        setRender={()=> setRender(!render)}
      />
      <div className='card'>
        <div className='card-heading'>Average: {average}</div>
        <table className='table table-striped' aria-labelledby='covidTable'>
          <thead>
            <tr>
              <th>Date</th>
              <th>County</th>
              <th>State</th>
              <th>Cases</th>
            </tr>
          </thead>
          <tbody>
            {dailyCounts.map(dailyCount =>
              <tr key={dailyCount.id}>
                <td>{dailyCount.date}</td>
                <td>{dailyCount.county}</td>
                <td>{dailyCount.state}</td>
                <td>{dailyCount.Cases}</td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </>
  );
}

export default DailyCountData;


