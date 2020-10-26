import React, { useState, useEffect } from 'react';
import axios from 'axios';
import authService from './api-authorization/AuthorizeService'
import { userUrl } from'./AppConstants';
import { Form, FormGroup, Label, Input } from 'reactstrap';

function ListForm({ setListId }) {
  const [user, setUser] = useState(null);

  useEffect(()=> {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const token = await authService.getAccessToken();
      const response = await axios.get(userUrl + 2, {
        headers: { 'Authorization': `Bearer ${token}` }
      })
      setUser(response.data);
    } catch (error) {
      console.log(error.message);
    }
  };

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
          {user && user.countLists.map(list => 
            <option value={list.id} key={list.id}>
              {list.id}
            </option>
          )}
        </Input>
      </FormGroup>
    </Form>
  );
}

export default ListForm;