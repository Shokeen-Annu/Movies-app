import React, { Component } from 'react';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { movies: [], loading: true };
    }

    componentDidMount() {
        this.populateMoviesData();
    }

    static renderMoviesTable(movies) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Title</th>
                        <th>Rating</th>
                        <th>Cheapest Provider</th>
                        <th>Price</th>
                    </tr>
                </thead>
                <tbody>
                    {movies && movies.length > 0 ? movies.map(movie =>
                        <tr key={movie.id}>
                            <td>{movie.title}</td>
                            <td>{movie.rating}</td>
                            <td>{movie.provider == 0 ? "Cinema World" : "Film World"}</td>
                            <td>{movie.price}</td>
                        </tr>
                    ) : <tr><td colSpan="4">Oops... No movies are available. Try again after sometime.</td></tr>}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderMoviesTable(this.state.movies);

        return (
            <div>
                <h1 id="tabelLabel" >Movies</h1>
                {contents}
            </div>
        );
    }

    async populateMoviesData() {
        const response = await fetch('api/movies');
        const data = await response.json();
        this.setState({ movies: data, loading: false });
    }
}
