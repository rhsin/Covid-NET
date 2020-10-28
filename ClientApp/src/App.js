import React from 'react';
import { Route, Switch } from 'react-router';
import Layout from './components/Layout';
import DailyCount from './components/DailyCount';
import CountList from './components/CountList';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import './custom.css';

function App() {
  return (
    <Layout>
      <Switch>
        <AuthorizeRoute exact path='/'>
          <DailyCount />
        </AuthorizeRoute>
        <AuthorizeRoute path='/count-list'>
          <CountList />
        </AuthorizeRoute>
        <Route path={ApplicationPaths.ApiAuthorizationPrefix}>
          <ApiAuthorizationRoutes />
        </Route>
      </Switch>
    </Layout>
  );
}

export default App;
