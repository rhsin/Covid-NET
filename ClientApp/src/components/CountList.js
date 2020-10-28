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

  const handleClick = (id) => {
    handleCount('Remove', listId, id);
    setRender();
  };

  return (
    <>
      {loading && <p><em>Loading...</em></p>}
      <div>
        <h1 id='covidTable'>COVID DailyCount Lists</h1>
        <p>This component demonstrates fetching data from the server.</p>
      </div>
      <ListForm
        users={users}
        user={user}
        selectUser={id => selectUser(id)}
        setListId={id => setListId(id)} 
      />
      {dailyCountList.map((dailyCounts, index) => dailyCounts.length > 0 &&
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
                  <Button
                    onClick={()=> handleClick(item.dailyCount.id)}
                  >
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


