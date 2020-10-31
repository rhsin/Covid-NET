import { dynamicCountList } from '../components/AppHelpers';

const dailyCountLists = [
  [
    {countListId: 1},
    {countListId: 1}
  ],
  [
    {countListId: 2},
    {countListId: 2}
  ]
];

const user = {name: 'Ryan'};

test('dynamicCountList filters countLists', () => {
  const dailyCountList = dynamicCountList(dailyCountLists, user, 1);
  expect(dailyCountList).toEqual([
    dailyCountLists[0], []
  ]);
});

test('dynamicCountList filters with null user', () => {
  const dailyCountList = dynamicCountList(dailyCountLists, null, 1);
  expect(dailyCountList).toEqual(dailyCountLists);
});

test('dynamicCountList filters with null listId', () => {
  const dailyCountList = dynamicCountList(dailyCountLists, user, null);
  expect(dailyCountList).toEqual([
    [], []
  ]);
});