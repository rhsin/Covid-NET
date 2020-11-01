import React from 'react';
import axios from 'axios';
import user from '@testing-library/user-event';
import { render, screen } from '@testing-library/react';
import { Context } from '../../components/Layout';
import DailyCount from '../../components/DailyCount';

jest.mock('axios');
axios.get.mockResolvedValueOnce([]);

const fetchCounts = jest.fn();
const store = {
  dailyCounts: [],
  users: [],
  loading: true,
  fetchCounts: ()=> fetchCounts()
};

test('renders loading alert', () => {  
  render(
    <Context.Provider value={store}>
      <DailyCount />
    </Context.Provider>
  );
  expect(screen.getByText(/Loading.../)).toBeInTheDocument();
});

test('submit button calls fetchCounts on click', () => {  
  render(
    <Context.Provider value={store}>
      <DailyCount />
    </Context.Provider>
  );
  user.type(screen.getByLabelText(/County/), 'San Diego');
  const submitButton = screen.getByRole('button', {name: /Submit/});
  user.click(submitButton);
  expect(fetchCounts).toHaveBeenCalledTimes(1);
});






