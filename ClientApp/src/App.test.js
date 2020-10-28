import React from 'react';
import ReactDOM from 'react-dom';
import { render } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import App from './App';
import DailyCount from './components/DailyCount';

test('renders without crashing', async () => {
  const div = document.createElement('div');
  ReactDOM.render(
    <MemoryRouter>
      <App />
    </MemoryRouter>, div);
  await new Promise(resolve => setTimeout(resolve, 1000));
});

// test('renders DailyCount title', () => {
//   const { getByText } = render(<DailyCount />);
//   const textElement = getByText(/COVID Daily Counts/);
//   expect(textElement).toBeInTheDocument();
// });

test('renders CountList title', () => {
  const { getByText } = render(<App />);
  const textElement = getByText(/COVID DailyCount Lists/);
  expect(textElement).toBeInTheDocument();
});

// test('renders search county input', () => {
//   const { getByText } = render(<App />);
//   const inputElement = getByText(/County/);
//   expect(inputElement).toBeInTheDocument();
// });

// test('renders search state input', () => {
//   const { getByText } = render(<App />);
//   const inputElement = getByText(/State/);
//   expect(inputElement).toBeInTheDocument();
// });

// test('renders month select', () => {
//   const { getByText } = render(<App />);
//   const selectElement = getByText(/Month/);
//   expect(selectElement).toBeInTheDocument();
// });

// test('renders order select', () => {
//   const { getByText } = render(<App />);
//   const selectElement = getByText(/Order/);
//   expect(selectElement).toBeInTheDocument();
// });

// test('renders list select', () => {
//   const { getByText } = render(<App />);
//   const selectElement = getByText(/Select List/);
//   expect(selectElement).toBeInTheDocument();
// });

// test('renders user select', () => {
//   const { getByText } = render(<App />);
//   const selectElement = getByText(/Select User/);
//   expect(selectElement).toBeInTheDocument();
// });

// test('renders submit button', () => {
//   const { getByText } = render(<App />);
//   const buttonElement = getByText(/Submit/);
//   expect(buttonElement).toBeInTheDocument();
// });

// test('renders table heading', () => {
//   const { getByText } = render(<App />);
//   const tableElement = getByText(/Date/);
//   expect(tableElement).toBeInTheDocument();
// });
