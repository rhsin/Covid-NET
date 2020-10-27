import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import DailyCount from './components/DailyCount';
import CountList from './components/CountList';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <AuthorizeRoute exact path='/'>
          <DailyCount />
        </AuthorizeRoute>
        <AuthorizeRoute path='/count-list'>
          <CountList />
        </AuthorizeRoute>
        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
      </Layout>
    );
  }
}
