import React, { useContext, useState } from 'react';
import SearchForm from './SearchForm';
import { Context } from './Layout';
import { Button } from 'reactstrap';

function DailyCount() {
  const { dailyCounts, loading, handleClick } = useContext(Context);

  const [listId, setListId] = useState(5);

  return (
    <>
      {loading && <p><em>Loading...</em></p>}
      <div>
        <h1 id='covidTable'>COVID Daily Counts</h1>
        <p>This component demonstrates fetching data from the server.</p>
      </div>
      <SearchForm setListId={id => setListId(id)} />
      <table className='table table-striped' aria-labelledby='covidTable'>
        <thead>
          <tr>
            <th>Date</th>
            <th>County</th>
            <th>State</th>
            <th>Cases</th>
            <th>Deaths</th>
            <th>Save</th>
          </tr>
        </thead>
        <tbody>
          {dailyCounts && dailyCounts.map(dailyCount =>
            <tr key={dailyCount.id}>
              <td>{dailyCount.date}</td>
              <td>{dailyCount.county}</td>
              <td>{dailyCount.state}</td>
              <td>{dailyCount.cases}</td>
              <td>{dailyCount.deaths}</td>
              <td>
                <Button
                  onClick={()=> handleClick('Add', listId, dailyCount.id)}
                >
                  Save
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


