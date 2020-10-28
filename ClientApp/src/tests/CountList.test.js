import React from 'react';
import { render } from '@testing-library/react';
import CountList from '../components/CountList';

test('renders CountList title', () => {
  const { getByText } = render(<CountList />);
  const textElement = getByText(/COVID DailyCount Lists/);
  expect(textElement).toBeInTheDocument();
});

test('renders user select', () => {
  const { getByLabelText } = render(<CountList />);
  const selectElement = getByLabelText(/Select User/);
  expect(selectElement).toBeInTheDocument();
});

test('renders list select', () => {
  const { getByLabelText } = render(<CountList />);
  const selectElement = getByLabelText(/Select List/);
  expect(selectElement).toBeInTheDocument();
});


