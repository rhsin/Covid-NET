import React from 'react';
import { render, screen } from '@testing-library/react';
import { Context } from '../../components/Layout';
import DailyCountData from '../../components/DailyCountData';

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










