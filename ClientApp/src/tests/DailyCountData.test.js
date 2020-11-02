import React from 'react';
import { render } from '@testing-library/react';
import DailyCountData from '../components/DailyCountData';

test('renders DailyCountData title', () => {
  const { getByText } = render(<DailyCountData />);
  const textElement = getByText(/COVID Daily Counts Data/);
  expect(textElement).toBeInTheDocument();
});

test('renders search county input', () => {
  const { getByPlaceholderText } = render(<DailyCountData />);
  const inputElement = getByPlaceholderText(/County/);
  expect(inputElement).toBeInTheDocument();
});

test('renders search state input', () => {
  const { getByPlaceholderText } = render(<DailyCountData />);
  const inputElement = getByPlaceholderText(/State/);
  expect(inputElement).toBeInTheDocument();
});

test('renders month select', () => {
  const { getByLabelText } = render(<DailyCountData />);
  const selectElement = getByLabelText(/Month/);
  expect(selectElement).toBeInTheDocument();
});

test('renders month option', () => {
  const { getByText } = render(<DailyCountData />);
  const optionElement = getByText(/January/);
  expect(optionElement).toBeInTheDocument();
});

test('renders submit button', () => {
  const { getByText } = render(<DailyCountData />);
  const buttonElement = getByText(/Submit/);
  expect(buttonElement).toBeInTheDocument();
});

test('renders card heading', () => {
  const { getByText } = render(<DailyCountData />);
  const textElement = getByText(/Average/);
  expect(textElement).toBeInTheDocument();
});

test('renders table heading', () => {
  const { getByText } = render(<DailyCountData />);
  const tableElement = getByText(/Date/);
  expect(tableElement).toBeInTheDocument();
});
