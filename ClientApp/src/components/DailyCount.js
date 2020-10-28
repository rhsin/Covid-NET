import React, { useContext, useState } from 'react';
import SearchForm from './SearchForm';
import { Context } from './Layout';
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
        <p>This component demonstrates fetching data from the server.</p>
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
            {dailyCounts && dailyCounts.map(dailyCount =>
              <tr key={dailyCount.id}>
                <td>{dailyCount.date}</td>
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


