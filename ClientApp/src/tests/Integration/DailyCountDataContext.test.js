import React from 'react';
import axios from 'axios';
import user from '@testing-library/user-event';
import { render, screen } from '@testing-library/react';
import { Context } from '../../components/Layout';
import DailyCountData from '../../components/DailyCountData';

jest.mock('axios');
axios.get.mockResolvedValue([{county: 'San Diego'}]);

const fetchCounts = jest.fn().mockResolvedValue([{county: 'San Diego'}]);
const setLoading = jest.fn();
const setRender = jest.fn();

const store = {
  loading: true,
  render: false,
  setLoading: ()=> setLoading(), 
  setRender: ()=> setRender()
};

test('renders loading alert', () => {  
  render(
    <Context.Provider value={store}>
      <DailyCountData />
    </Context.Provider>
  );
  expect(screen.getByText(/Loading.../)).toBeInTheDocument();
});

// test('submit button calls fetchCounts on click', () => {  
//   render(
//     <Context.Provider value={store}>
//       <DailyCountData fetchCounts={()=> fetchCounts()} />
//     </Context.Provider>
//   );
//   const submitButton = screen.getByRole('button', {name: /Submit/});
//   user.click(submitButton);
//   expect(fetchCounts).toHaveBeenCalledTimes(1);
// });








