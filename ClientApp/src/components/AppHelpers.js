
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
