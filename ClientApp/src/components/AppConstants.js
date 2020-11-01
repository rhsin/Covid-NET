
export const url = 'https://localhost:44321/api/DailyCounts/DateRange/9';

export const listUrl = 'https://localhost:44321/api/CountLists/';

export const userUrl = 'https://localhost:44321/api/AppUsers/';

export const monthList = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September'];

export const urlQuery = (county, state, month, order) => (
  `https://localhost:44321/api/DailyCounts/Query?&county=${county}&state=${state}&month=${month}&order=${order}`
);

export const urlData = (county, state, month) => (
  `https://localhost:44321/api/Query/Data/Cases/${month}?&county=${county}&state=${state}`
);