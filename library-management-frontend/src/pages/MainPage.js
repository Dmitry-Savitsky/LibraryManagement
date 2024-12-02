import React, { useEffect, useState } from "react";
import { Carousel, Card, Button, Container, Row, Col } from "react-bootstrap";
import { getAllAuthors } from "../http/authorApi";
import { getAllBookCharacteristics } from "../http/bookCharacteristicsApi";

const MainPage = () => {
  const [authors, setAuthors] = useState([]);
  const [books, setBooks] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const authorsData = await getAllAuthors();
        const booksData = await getAllBookCharacteristics();
        setAuthors(authorsData);
        setBooks(booksData);
      } catch (error) {
        console.error("Error fetching data:", error);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, []);

  if (loading) {
    return <div className="text-center mt-5">Loading...</div>;
  }

  return (
    <Container className="mt-5">
      <Carousel>
        {books.map((book) => (
          <Carousel.Item key={book.id}>
            <img
              className="d-block w-100"
              src={process.env.REACT_APP_API_URL + book.imgPath}
              alt={book.title}
              style={{ height: "400px", objectFit: "cover" }}
            />
            <Carousel.Caption>
              <h5>{book.title}</h5>
              <p>{book.description.substring(0, 100)}...</p>
            </Carousel.Caption>
          </Carousel.Item>
        ))}
      </Carousel>

      <h2 className="my-4">Популярное</h2>
      <Row>
        {books.map((book) => (
          <Col key={book.id} sm={12} md={6} lg={4} className="mb-4">
            <Card>
              <Card.Img
                variant="top"
                src={process.env.REACT_APP_API_URL + book.imgPath}
                style={{ height: "200px", objectFit: "cover" }}
              />
              <Card.Body>
                <Card.Title>{book.title}</Card.Title>
                <Card.Text>
                  <strong>Жанр:</strong> {book.genre}
                  <br />
                  <strong>Описание:</strong> {book.description.substring(0, 100)}...
                </Card.Text>
              </Card.Body>
            </Card>
          </Col>
        ))}
      </Row>
    </Container>
  );
};

export default MainPage;
