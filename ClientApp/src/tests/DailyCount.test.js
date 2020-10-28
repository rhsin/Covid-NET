import React from 'react';
import { render } from '@testing-library/react';
import DailyCount from '../components/DailyCount';

test('renders DailyCount title', () => {
  const { getByText } = render(<DailyCount />);
  const textElement = getByText(/COVID Daily Counts/);
  expect(textElement).toBeInTheDocument();
});

test('renders search county input', () => {
  const { getByPlaceholderText } = render(<DailyCount />);
  const inputElement = getByPlaceholderText(/County/);
  expect(inputElement).toBeInTheDocument();
});

test('renders search state input', () => {
  const { getByPlaceholderText } = render(<DailyCount />);
  const inputElement = getByPlaceholderText(/State/);
  expect(inputElement).toBeInTheDocument();
});

test('renders month select', () => {
  const { getByLabelText } = render(<DailyCount />);
  const selectElement = getByLabelText(/Month/);
  expect(selectElement).toBeInTheDocument();
});

test('renders month option', () => {
  const { getByText } = render(<DailyCount />);
  const optionElement = getByText(/January/);
  expect(optionElement).toBeInTheDocument();
});

test('renders order select', () => {
  const { getByLabelText } = render(<DailyCount />);
  const selectElement = getByLabelText(/Order/);
  expect(selectElement).toBeInTheDocument();
});

test('renders order option', () => {
  const { getByText } = render(<DailyCount />);
  const optionElement = getByText(/Descending/);
  expect(optionElement).toBeInTheDocument();
});

test('renders list select', () => {
  const { getByLabelText } = render(<DailyCount />);
  const selectElement = getByLabelText(/Select List/);
  expect(selectElement).toBeInTheDocument();
});

test('renders submit button', () => {
  const { getByText } = render(<DailyCount />);
  const buttonElement = getByText(/Submit/);
  expect(buttonElement).toBeInTheDocument();
});

test('renders table heading', () => {
  const { getByText } = render(<DailyCount />);
  const tableElement = getByText(/Save/);
  expect(tableElement).toBeInTheDocument();
});
