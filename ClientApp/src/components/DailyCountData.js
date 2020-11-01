// This component fetches the daily & average case count for the selected county, state, month.
// The imported urlData function returns a url string from the parameters passed 
// to a template literal.

import React, { useContext, useState, useEffect } from 'react';
import axios from 'axios';
import DataForm from './DataForm';
import { Context } from './Layout';
import { urlData } from'./AppConstants';

function DailyCountData() {
  const { loading, render, setLoading, setRender } = useContext(Context);

  const [dailyCounts, setDailyCounts] = useState([]);
  const [average, setAverage] = useState(null);
  const [county, setCounty] = useState('San Diego');
  const [state, setState] = useState('');
  const [month, setMonth] = useState(9);

  useEffect(()=> {
    fetchData();
    // eslint-disable-next-line
  }, [render]);

  const fetchData = async () => {
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
        setCounty={county => setCounty(county)}
        setState={state => setState(state)}
        setMonth={month => setMonth(month)}
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
              <tr key={dailyCount.Id}>
                <td>{dailyCount.Date}</td>
                <td>{dailyCount.County}</td>
                <td>{dailyCount.State}</td>
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


