
export const dynamicCountList = (dailyCountLists, user, listId) => {
  if (user) {
    return dailyCountLists.map(dailyCounts => 
      dailyCounts.filter(dailyCount => dailyCount.countListId == listId));
  } 
  else {
    return dailyCountLists;
  }
};
