import React from 'react';
import axios from 'axios';
import user from '@testing-library/user-event';
import { render, screen } from '@testing-library/react';
import { Context } from '../../components/Layout';
import CountList from '../../components/CountList';

jest.mock('axios');
axios.get.mockResolvedValue([{county: 'San Diego'}]);

const handleCount = jest.fn();
const setRender = jest.fn();

const store = {
  countLists: [{
    countListDailyCounts: [{
      dailyCount: {cases: 500}
    }]
  }],
  users: [],
  loading: true,
  handleCount: ()=> handleCount(),
  setRender: ()=> setRender()
};

test('renders loading alert', () => {  
  render(
    <Context.Provider value={store}>
      <CountList />
    </Context.Provider>
  );
  expect(screen.getByText(/Loading.../)).toBeInTheDocument();
});

test('renders countLists from Context', () => {  
  render(
    <Context.Provider value={store}>
      <CountList />
    </Context.Provider>
  );
  expect(screen.getByText(/500/)).toBeInTheDocument();
});

test('remove button renders after countLists fetched', () => {  
  render(
    <Context.Provider value={store}>
      <CountList />
    </Context.Provider>
  );
  const removeButton = screen.getByRole('button', {name: /Remove/});
  expect(removeButton).toBeInTheDocument();
});

test('remove button calls handleCount on click', () => {  
  render(
    <Context.Provider value={store}>
      <CountList />
    </Context.Provider>
  );
  const removeButton = screen.getByRole('button', {name: /Remove/});
  user.click(removeButton);
  expect(handleCount).toHaveBeenCalledTimes(1);
});







