import React from 'react';
import { Form, FormGroup, Label, Input } from 'reactstrap';

function ListForm({ setListId }) {
  return (
    <Form className='form-search'>
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
          <option value={8}>8</option>
          <option value={9}>9</option>
        </Input>
      </FormGroup>
    </Form>
  );
}

export default ListForm;