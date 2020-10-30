
export const dynamicCountList = (user, dailyCountList, listId) => {
  if (user) {
    return dailyCountList.map(dailyCounts => 
      dailyCounts.filter(dailyCount => dailyCount.countListId == listId));
  } 
  else {
    return dailyCountList;
  }
};
