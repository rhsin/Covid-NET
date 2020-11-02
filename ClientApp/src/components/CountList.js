// The handleCount function is passed through Context to delete
// entry in join table (CountList & DailyCount)

import React, { useContext, useState } from 'react';
import ListForm from './ListForm';
import { Context } from './Layout';
import { dynamicCountList } from './AppHelpers';
import { Button } from 'reactstrap';

function CountList() {
  const { countLists, users, loading, handleCount, setRender } = useContext(Context);

  const [user, setUser] = useState(null);
  const [listId, setListId] = useState(null);

  // The dailyCountList uses a helper funtion to filter the list based on the user & listId.
  // When user is selected the CountList table will only show the CountList matching the selected list.
  const dailyCountLists = countLists.map(countList => countList.countListDailyCounts);
  const dailyCountList = dynamicCountList(dailyCountLists, user, listId);
    
  const selectUser = (id) => {
    // eslint-disable-next-line
    const user = users.find(user => user.accountId == id);
    if (user) {
      setUser(user);
    } 
    else {
      setUser(null);
    }
  };

  // CountList tables will only be displayed (when user is selected) if not empty
  const showTable = (list) => {
    if (user) {
      return list.length > 0;
    } 
    return true;
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
      </div>
      <ListForm
        users={users}
        user={user}
        selectUser={id => selectUser(id)}
        setListId={id => setListId(id)} 
      />
      <div className='card'>
        {dailyCountList.map((dailyCounts, index) => showTable(dailyCounts) &&
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
              {dailyCounts.map(item => 
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


