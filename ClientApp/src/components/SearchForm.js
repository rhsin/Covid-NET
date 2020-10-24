import React, { useState } from 'react';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';

function SearchForm(props) {
  const { setLoading, fetchData } = props;

  const [county, setCounty] = useState('');
  const [state, setState] = useState('');
  const [month, setMonth] = useState(null);
  const [order, setOrder] = useState('');

  const monthList = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September'];

  const url = `https://localhost:44321/api/DailyCounts/Query?&county=${county}&state=${state}&month=${month}&order=${order}`;

  const handleSubmit = (e) => {
    e.preventDefault();
    setLoading();
    fetchData(url);
  };

  return (
    <Form className='form-search'>
      <FormGroup>
        <Label for="inputCounty">County</Label>
        <Input
          type="text" 
          name="county"
          onChange={e => setCounty(e.target.value)} 
          id= "inputCounty" 
          placeholder="Enter County" 
        />
      </FormGroup>
      <FormGroup>
        <Label for="inputState">State</Label>
        <Input 
          type="text" 
          name="state" 
          onChange={e => setState(e.target.value)} 
          id="inputState" 
          placeholder="Enter State" 
        />
      </FormGroup>
      <FormGroup>
        <Label for="selectMonth">Month</Label>
        <Input
          type="select"
          name="month"
          onChange={e => setMonth(parseInt(e.target.value))} 
          id="selectMonth"
        >
          {monthList.map((month, index) => 
            <option value={index + 1} key={index}>
              {month}
            </option>  
          )}
        </Input>
      </FormGroup>
      <FormGroup>
        <Label for="selectOrder">Order</Label>
        <Input 
          type="select"
          name="order" 
          onChange={e => setOrder(e.target.value)} 
          id="selectOrder"
        >
          <option value=''>Ascending</option>
          <option value='desc'>Descending</option>
        </Input>
      </FormGroup>
      <Button onClick={e => handleSubmit(e)}>
        Submit
      </Button>
    </Form>
  );
}

export default SearchForm;