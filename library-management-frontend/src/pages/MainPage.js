import React, { useEffect, useState } from "react";
import { Row, Col, Card, Button } from "react-bootstrap";
import { getPaginatedBookCharacteristics } from "../http/bookCharacteristicsApi";

const MainPage = () => {
  const [books, setBooks] = useState([]);
  const [pageNumber, setPageNumber] = useState(1);
  const pageSize = 6;
  const [totalCount, setTotalCount] = useState(0);

  useEffect(() => {
    const fetchBooks = async () => {
      try {
        const data = await getPaginatedBookCharacteristics(pageNumber, pageSize);
        setBooks(data.items);
        setTotalCount(data.totalCount);
      } catch (error) {
        console.error("Ошибка загрузки книг:", error);
      }
    };

    fetchBooks();
  }, [pageNumber]);

  const totalPages = Math.ceil(totalCount / pageSize);

  return (
    <div className="mx-5 px-3" >
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

      <div className="d-flex justify-content-center mt-4">
        <Button
          variant="primary"
          disabled={pageNumber === 1}
          onClick={() => setPageNumber(pageNumber - 1)}
          className="me-2"
        >
          Назад
        </Button>
        <span className="align-self-center">
          Страница {pageNumber} из {totalPages}
        </span>
        <Button
          variant="primary"
          disabled={pageNumber >= totalPages}
          onClick={() => setPageNumber(pageNumber + 1)}
          className="ms-2"
        >
          Вперед
        </Button>
      </div>
    </div>
  );
};

export default MainPage;
