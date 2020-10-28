import React from 'react';
import ReactDOM from 'react-dom';
import { render } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import App from '../App';

test('renders without crashing', async () => {
  const div = document.createElement('div');
  ReactDOM.render(
    <MemoryRouter>
      <App />
    </MemoryRouter>, div);
  await new Promise(resolve => setTimeout(resolve, 1000));
});

test('renders home link', () => {
  const { getByText } = render(<App />);
  const linkElement = getByText(/Covid/);
  expect(linkElement).toBeInTheDocument();
});

test('renders DailyCount link', () => {
  const { getByText } = render(<App />);
  const linkElement = getByText(/Daily Count/);
  expect(linkElement).toBeInTheDocument();
});

test('renders CountList link', () => {
  const { getByText } = render(<App />);
  const linkElement = getByText(/Count List/);
  expect(linkElement).toBeInTheDocument();
});

test('renders Login link', () => {
  const { getByText } = render(<App />);
  const linkElement = getByText(/Login/);
  expect(linkElement).toBeInTheDocument();
});

