
export const urlQuery = (county, state, month, order) => (
  `https://localhost:44321/api/DailyCounts/Query?&county=${county}&state=${state}&month=${month}&order=${order}`
);

export const urlData = (county, state, month) => (
  `https://localhost:44321/api/Query/Data/Cases/${month}?&county=${county}&state=${state}`
);

export const dynamicCountList = (dailyCountLists, user, listId) => {
  if (user) {
    return dailyCountLists.map(dailyCounts => 
      // eslint-disable-next-line
      dailyCounts.filter(dailyCount => dailyCount.countListId == listId));
  } 
  else {
    return dailyCountLists;
  }
};

export const dateFormat = (date) => (date.slice(0, 10));
