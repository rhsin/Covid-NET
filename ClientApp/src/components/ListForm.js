// The SelectList form shows the countList Id for the selected user ONLY
// when selected by the SelectUser form, no defaults.

import React from 'react';
import { Form, FormGroup, Label, Input } from 'reactstrap';

function ListForm(props) {
  const { users, user, selectUser, setListId } = props;
  
  return (
    <div className='card'>
      <Form className='form-search'>
        <FormGroup>
          <Label for='selectUser'>Select User</Label>
          <Input 
            type='select'
            name='user' 
            onChange={e => selectUser(e.target.value)} 
            id='selectUser'
          >
            <option value={null}>
              All
            </option>
            {users.map(user => 
              <option value={user.accountId} key={user.accountId}>
                {user.name}
              </option>
            )}
          </Input>
        </FormGroup>  
        <FormGroup>
          <Label for='selectList'>Select List</Label>
          <Input 
            type='select'
            name='list' 
            onChange={e => setListId(e.target.value)} 
            id='selectList'
          >
            {user && user.countLists.map(list => 
              <option value={list.id} key={list.id}>
                {list.id}
              </option>
            )}
          </Input>
        </FormGroup>
      </Form>
    </div>
  );
}

export default ListForm;