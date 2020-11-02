// The imported urlQuery function returns a url string from the parameters passed 
// to a template literal. Then the fetchCounts function passed through Context updates
// the global dailyCounts. 

import React, { useContext, useState } from 'react';
import { Context } from './Layout';
import { monthList } from'./AppConstants';
import { urlQuery } from'./AppHelpers';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';

function SearchForm({ setListId }) {
  const { users, fetchCounts } = useContext(Context);

  const [county, setCounty] = useState('');
  const [state, setState] = useState('');
  const [month, setMonth] = useState(1);
  const [order, setOrder] = useState('');

  const handleClick = () => {
    fetchCounts(urlQuery(county, state, month, order));
  };

  return (
    <>
      <div className='card'>
        <Form className='form-search'>
          <FormGroup>
            <Label for='inputCounty'>County</Label>
            <Input
              type='text' 
              name='county'
              onChange={e => setCounty(e.target.value)} 
              id= 'inputCounty' 
              placeholder='Enter County' 
            />
          </FormGroup>
          <FormGroup>
            <Label for='inputState'>State</Label>
            <Input 
              type='text' 
              name='state' 
              onChange={e => setState(e.target.value)} 
              id='inputState' 
              placeholder='Enter State' 
            />
          </FormGroup>
          <FormGroup>
            <Label for='selectMonth'>Month</Label>
            <Input
              type='select'
              name='month'
              onChange={e => setMonth(parseInt(e.target.value))} 
              id='selectMonth'
            >
              {monthList.map((month, index) => 
                <option value={index + 1} key={index}>
                  {month}
                </option>  
              )}
            </Input>
          </FormGroup>
          <FormGroup>
            <Label for='selectOrder'>Order</Label>
            <Input 
              type='select'
              name='order' 
              onChange={e => setOrder(e.target.value)} 
              id='selectOrder'
            >
              <option value=''>Ascending</option>
              <option value='desc'>Descending</option>
            </Input>
          </FormGroup>
          <Button
            onClick={()=> handleClick()}
            color='primary'
          >
            Submit
          </Button>
        </Form>
      </div>
      <Form className='form-search'>
        <FormGroup>
          <Label for='selectList'>Select List</Label>
          <Input 
            type='select'
            name='list' 
            onChange={e => setListId(e.target.value)} 
            id='selectList'
          >
            {users.map(user => 
              user.countLists.map(list => 
                <option value={list.id} key={list.id}>
                  {list.id}
                </option>
            ))}    
          </Input>
        </FormGroup>
      </Form>
    </>
  );
}

export default SearchForm;