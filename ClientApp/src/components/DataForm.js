import React, { useState } from 'react';
import { monthList } from'./AppConstants';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';

function DataForm({ fetchCounts, setRender }) {
  const [county, setCounty] = useState('');
  const [state, setState] = useState('');
  const [month, setMonth] = useState(1);

  const handleClick = () => {
    fetchCounts(county, state, month);
    setRender();
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
          <Button
            onClick={()=> handleClick()}
            color='primary'
          >
            Submit
          </Button>
        </Form>
      </div>
    </>
  );
}

export default DataForm;