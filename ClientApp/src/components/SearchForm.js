import React, { useState } from 'react';
import { urlQuery, monthList } from'./AppConstants';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';

function SearchForm(props) {
  const { setLoading, fetchData, setListId } = props;

  const [county, setCounty] = useState('');
  const [state, setState] = useState('');
  const [month, setMonth] = useState(null);
  const [order, setOrder] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    setLoading();
    fetchData(urlQuery(county, state, month, order));
  };

  return (
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
      <Button onClick={e => handleSubmit(e)} id='submit'>
        Submit
      </Button>
      <FormGroup>
        <Label for='selectList'>Select List</Label>
        <Input 
          type='select'
          name='list' 
          onChange={e => setListId(e.target.value)} 
          id='selectList'
        >
          <option value={5}>5</option>
          <option value={6}>6</option>
          <option value={7}>7</option>
        </Input>
      </FormGroup>
    </Form>
  );
}

export default SearchForm;