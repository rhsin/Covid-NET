// This app component was modified from .NET Core's React template, using updated npm packages
// and converting to a functional component

import React from 'react';
import { Route, Switch, BrowserRouter } from 'react-router-dom';
import Layout from './components/Layout';
import DailyCount from './components/DailyCount';
import CountList from './components/CountList';
import DailyCountData from './components/DailyCountData';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import './custom.css';

function App() {
  return (
    <BrowserRouter>
      <Layout>
        <Switch>
          <AuthorizeRoute exact path='/'>
            <DailyCount />
          </AuthorizeRoute>
          <AuthorizeRoute path='/count-list'>
            <CountList />
          </AuthorizeRoute>
          <AuthorizeRoute path='/count-data'>
            <DailyCountData />
          </AuthorizeRoute>
          <Route path={ApplicationPaths.ApiAuthorizationPrefix}>
            <ApiAuthorizationRoutes />
          </Route>
        </Switch>
      </Layout>
    </BrowserRouter>
  );
}

export default App;
