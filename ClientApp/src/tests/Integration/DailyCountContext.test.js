import React from 'react';
import axios from 'axios';
import user from '@testing-library/user-event';
import { render, screen } from '@testing-library/react';
import { Context } from '../../components/Layout';
import DailyCount from '../../components/DailyCount';

jest.mock('axios');
axios.get.mockResolvedValue([{county: 'San Diego'}]);

const fetchCounts = jest.fn().mockResolvedValue([{county: 'San Diego'}]);
const handleCount = jest.fn();
const setRender = jest.fn();

const store = {
  dailyCounts: [{county: 'San Diego'}],
  users: [],
  loading: true,
  fetchCounts: ()=> fetchCounts(),
  handleCount: ()=> handleCount(),
  setRender: ()=> setRender()
};

test('renders loading alert', () => {  
  render(
    <Context.Provider value={store}>
      <DailyCount />
    </Context.Provider>
  );
  expect(screen.getByText(/Loading.../)).toBeInTheDocument();
});

test('renders dailyCounts from Context', () => {  
  render(
    <Context.Provider value={store}>
      <DailyCount />
    </Context.Provider>
  );
  expect(screen.getByText(/San Diego/)).toBeInTheDocument();
});

test('submit button calls fetchCounts on click', () => {  
  render(
    <Context.Provider value={store}>
      <DailyCount />
    </Context.Provider>
  );
  const submitButton = screen.getByRole('button', {name: /Submit/});
  user.click(submitButton);
  expect(fetchCounts).toHaveBeenCalledTimes(1);
});

test('save button renders after dailyCounts fetched', () => {  
  render(
    <Context.Provider value={store}>
      <DailyCount />
    </Context.Provider>
  );
  user.type(screen.getByLabelText(/County/), 'San Diego');
  const submitButton = screen.getByRole('button', {name: /Submit/});
  user.click(submitButton);
  const saveButton = screen.getByRole('button', {name: /Save/});
  expect(saveButton).toBeInTheDocument();
});

test('save button calls handleCount on click', () => {  
  render(
    <Context.Provider value={store}>
      <DailyCount />
    </Context.Provider>
  );
  const saveButton = screen.getByRole('button', {name: /Save/});
  user.click(saveButton);
  expect(fetchCounts).toHaveBeenCalledTimes(2);
  expect(handleCount).toHaveBeenCalledTimes(1);
});







