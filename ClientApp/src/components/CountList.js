// The handleCount function is passed through Context to delete
// entry in join table (CountList & DailyCount)

import React, { useContext, useState } from 'react';
import ListForm from './ListForm';
import { Context } from './Layout';
import { Button } from 'reactstrap';

function CountList() {
  const { countLists, users, loading, handleCount, setRender } = useContext(Context);

  const [user, setUser] = useState(null);
  const [listId, setListId] = useState(null);

  const dailyCountList = countLists.map(countList => countList.countListDailyCounts);
    
  const selectUser = (id) => {
    // eslint-disable-next-line
    const user = users.find(user => user.accountId == id);
    setUser(user);
  };

  const handleClick = (listId, id) => {
    handleCount('Remove', listId, id);
    setRender();
  };

  return (
    <>
      {loading && <p><em>Loading...</em></p>}
      <div>
        <h1 className='covidTable'>COVID DailyCount Lists</h1>
        <p>This component demonstrates fetching data from the server.</p>
      </div>
      <ListForm
        users={users}
        user={user}
        selectUser={id => selectUser(id)}
        setListId={id => setListId(id)} 
      />
      <div className='card'>
        {dailyCountList.map((dailyCounts, index) => dailyCounts.length > 0 &&
          <table 
            key={index} 
            className='table table-striped' 
            aria-labelledby='covidTable'
          >
            <thead>
              <tr>
                <th>List</th>
                <th>Date</th>
                <th>County</th>
                <th>State</th>
                <th>Cases</th>
                <th>Deaths</th>
                <th>Remove</th>
              </tr>
            </thead>
            <tbody>
              {dailyCounts.map(item => item.countListId == listId &&
                <tr key={item.dailyCount.id}>
                  <td>{item.countListId}</td>
                  <td>{item.dailyCount.date}</td>
                  <td>{item.dailyCount.county}</td>
                  <td>{item.dailyCount.state}</td>
                  <td>{item.dailyCount.cases}</td>
                  <td>{item.dailyCount.deaths}</td>
                  <td>
                    <Button
                      onClick={()=> handleClick(item.countListId, item.dailyCount.id)}
                      color='danger'
                    >
                      Remove
                    </Button>
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        )}
      </div>
    </>
  );
}

export default CountList;


