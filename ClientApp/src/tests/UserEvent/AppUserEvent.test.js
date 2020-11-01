import React from 'react';
import user from '@testing-library/user-event';
import { render, screen } from '@testing-library/react';
import { createMemoryHistory } from 'history';
import { Router, MemoryRouter } from 'react-router-dom';
import { Context } from '../../components/Layout';
import App from '../../App';
import DailyCount from '../../components/DailyCount';

// test('renders loading alert on submit', async () => {
//   const fetchCounts = jest.fn();
//   render(
//     <Context.Provider value={fetchCounts}>
//       <DailyCount />
//     </Context.Provider>
//   );

//   user.type(screen.getByLabelText(/County/), 'San Diego');
//   const submitButton = screen.getByRole('button', {name: /Submit/});
//   user.click(submitButton);

//   await screen.getAllByText(/Loading/);
// });

// test('full app rendering/navigating', () => {
//   const history = createMemoryHistory();
//   render(
//     <Router history={history}>
//       <App />
//     </Router>
//   );

//   const leftClick = { button: 0 };
//   userEvent.click(screen.getByText(/Covid/i), leftClick);

//   expect(screen.getByText(/COVID Daily Counts/i)).toBeInTheDocument();
// });



