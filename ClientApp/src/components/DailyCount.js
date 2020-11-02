// The handleCount function is passed through Context to create
// entry in join table (CountList & DailyCount)

import React, { useContext, useState } from 'react';
import SearchForm from './SearchForm';
import { Context } from './Layout';
import { dateFormat } from './AppHelpers';
import { Button } from 'reactstrap';

function DailyCount() {
  const { dailyCounts, loading, handleCount, setRender } = useContext(Context);

  const [listId, setListId] = useState(5);

  const handleClick = (id) => {
    handleCount('Add', listId, id);
    setRender();
  };

  return (
    <>
      {loading && <p><em>Loading...</em></p>}
      <div>
        <h1 className='covidTable'>COVID Daily Counts</h1>
      </div>
      <SearchForm setListId={id => setListId(id)} />
      <div className='card'>
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
            {dailyCounts.map(dailyCount =>
              <tr key={dailyCount.id}>
                <td>{dateFormat(dailyCount.date)}</td>
                <td>{dailyCount.county}</td>
                <td>{dailyCount.state}</td>
                <td>{dailyCount.cases}</td>
                <td>{dailyCount.deaths}</td>
                <td>
                  <Button 
                    onClick={()=> handleClick(dailyCount.id)}
                    color='success'
                  >
                    Save
                  </Button>
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </>
  );
}

export default DailyCount;


