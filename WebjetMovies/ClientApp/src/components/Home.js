import React, { Component } from 'react';
import { NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div>
                <h1>Hello, world!</h1>
                <p>Welcome to Webjet Movies!</p>

                <p>Click the below link to view all the available movies</p>
                <p><strong><NavLink tag={Link} className="fetchLink" to="/fetch-data">Fetch Movies</NavLink></strong></p>

            </div>
        );
    }
}
