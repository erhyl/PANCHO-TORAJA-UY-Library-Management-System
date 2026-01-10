-- Library Management System - Stored Procedures
-- Execute this script to create stored procedures for complex operations

DELIMITER $$

-- Stored Procedure: Borrow Book
-- Handles book borrowing with transaction management
DROP PROCEDURE IF EXISTS sp_BorrowBook$$
CREATE PROCEDURE sp_BorrowBook(
    IN p_MemberID INT,
    IN p_BookID INT,
    IN p_BorrowDate DATETIME,
    IN p_DueDate DATETIME,
    OUT p_TransactionID INT,
    OUT p_Success BIT,
    OUT p_Message VARCHAR(255)
)
BEGIN
    DECLARE v_Available INT DEFAULT 0;
    DECLARE v_BookType VARCHAR(50);
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SET p_Success = 0;
        SET p_Message = CONCAT('Error: ', SQLERRM);
    END;

    START TRANSACTION;

    -- Check book availability
    SELECT Available, BookType INTO v_Available, v_BookType
    FROM Books
    WHERE BookID = p_BookID;

    IF v_Available <= 0 THEN
        SET p_Success = 0;
        SET p_Message = 'Book is not available';
        ROLLBACK;
    ELSEIF v_BookType = 'Reference' THEN
        SET p_Success = 0;
        SET p_Message = 'Reference books cannot be borrowed';
        ROLLBACK;
    ELSE
        -- Insert transaction
        INSERT INTO Transactions (MemberID, BookID, BorrowDate, DueDate, Status, TransactionType)
        VALUES (p_MemberID, p_BookID, p_BorrowDate, p_DueDate, 'Borrowed', 'Borrow');

        SET p_TransactionID = LAST_INSERT_ID();

        -- Update book availability
        UPDATE Books
        SET Available = Available - 1
        WHERE BookID = p_BookID;

        SET p_Success = 1;
        SET p_Message = 'Book borrowed successfully';
        COMMIT;
    END IF;
END$$

-- Stored Procedure: Return Book
-- Handles book return with fine calculation
DROP PROCEDURE IF EXISTS sp_ReturnBook$$
CREATE PROCEDURE sp_ReturnBook(
    IN p_TransactionID INT,
    IN p_ReturnDate DATETIME,
    IN p_FineRatePerDay DECIMAL(10,2),
    IN p_MaxFineCap DECIMAL(10,2),
    OUT p_Success BIT,
    OUT p_FineAmount DECIMAL(10,2),
    OUT p_Message VARCHAR(255)
)
BEGIN
    DECLARE v_BookID INT;
    DECLARE v_DueDate DATETIME;
    DECLARE v_DaysOverdue INT DEFAULT 0;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SET p_Success = 0;
        SET p_FineAmount = 0;
        SET p_Message = CONCAT('Error: ', SQLERRM);
    END;

    START TRANSACTION;

    -- Get transaction details
    SELECT BookID, DueDate INTO v_BookID, v_DueDate
    FROM Transactions
    WHERE TransactionID = p_TransactionID AND Status = 'Borrowed';

    IF v_BookID IS NULL THEN
        SET p_Success = 0;
        SET p_FineAmount = 0;
        SET p_Message = 'Transaction not found or already returned';
        ROLLBACK;
    ELSE
        -- Calculate fine if overdue
        IF p_ReturnDate > v_DueDate THEN
            SET v_DaysOverdue = DATEDIFF(p_ReturnDate, v_DueDate);
            SET p_FineAmount = LEAST(v_DaysOverdue * p_FineRatePerDay, p_MaxFineCap);
        ELSE
            SET p_FineAmount = 0;
        END IF;

        -- Update transaction
        UPDATE Transactions
        SET ReturnDate = p_ReturnDate,
            Status = 'Returned',
            Fine = p_FineAmount
        WHERE TransactionID = p_TransactionID;

        -- Update book availability
        UPDATE Books
        SET Available = Available + 1
        WHERE BookID = v_BookID;

        SET p_Success = 1;
        SET p_Message = 'Book returned successfully';
        COMMIT;
    END IF;
END$$

-- Stored Procedure: Process Payment
-- Handles fine payment with transaction management
DROP PROCEDURE IF EXISTS sp_ProcessPayment$$
CREATE PROCEDURE sp_ProcessPayment(
    IN p_TransactionID INT,
    IN p_MemberID INT,
    IN p_AmountPaid DECIMAL(10,2),
    IN p_PaymentMode VARCHAR(50),
    IN p_ProcessedBy VARCHAR(100),
    OUT p_ReceiptNumber VARCHAR(50),
    OUT p_Success BIT,
    OUT p_Message VARCHAR(255)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SET p_Success = 0;
        SET p_ReceiptNumber = '';
        SET p_Message = CONCAT('Error: ', SQLERRM);
    END;

    START TRANSACTION;

    -- Generate receipt number
    SET p_ReceiptNumber = CONCAT('RCP-', DATE_FORMAT(NOW(), '%Y%m%d'), '-', LPAD(FLOOR(RAND() * 10000), 4, '0'));

    -- Insert payment record
    INSERT INTO FinePayments (TransactionID, MemberID, AmountPaid, PaymentMode, PaymentDate, ReceiptNumber, ProcessedBy)
    VALUES (p_TransactionID, p_MemberID, p_AmountPaid, p_PaymentMode, NOW(), p_ReceiptNumber, p_ProcessedBy);

    -- Update transaction fine status
    UPDATE Transactions
    SET Fine = GREATEST(0, Fine - p_AmountPaid)
    WHERE TransactionID = p_TransactionID;

    SET p_Success = 1;
    SET p_Message = 'Payment processed successfully';
    COMMIT;
END$$

-- Stored Procedure: Renew Book
-- Handles book renewal with eligibility checks
DROP PROCEDURE IF EXISTS sp_RenewBook$$
CREATE PROCEDURE sp_RenewBook(
    IN p_TransactionID INT,
    IN p_NewDueDate DATETIME,
    IN p_MaxRenewals INT,
    OUT p_Success BIT,
    OUT p_Message VARCHAR(255)
)
BEGIN
    DECLARE v_CurrentRenewals INT DEFAULT 0;
    DECLARE v_BookID INT;
    DECLARE v_HasReservations INT DEFAULT 0;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SET p_Success = 0;
        SET p_Message = CONCAT('Error: ', SQLERRM);
    END;

    START TRANSACTION;

    -- Get current renewal count and book ID
    SELECT RenewalCount, BookID INTO v_CurrentRenewals, v_BookID
    FROM Transactions
    WHERE TransactionID = p_TransactionID AND Status = 'Borrowed';

    IF v_BookID IS NULL THEN
        SET p_Success = 0;
        SET p_Message = 'Transaction not found or book already returned';
        ROLLBACK;
    ELSEIF v_CurrentRenewals >= p_MaxRenewals THEN
        SET p_Success = 0;
        SET p_Message = 'Maximum renewal limit reached';
        ROLLBACK;
    ELSE
        -- Check for pending reservations
        SELECT COUNT(*) INTO v_HasReservations
        FROM Reservations
        WHERE BookID = v_BookID AND Status = 'Active';

        IF v_HasReservations > 0 THEN
            SET p_Success = 0;
            SET p_Message = 'Book has pending reservations and cannot be renewed';
            ROLLBACK;
        ELSE
            -- Update transaction
            UPDATE Transactions
            SET DueDate = p_NewDueDate,
                RenewalCount = RenewalCount + 1
            WHERE TransactionID = p_TransactionID;

            SET p_Success = 1;
            SET p_Message = 'Book renewed successfully';
            COMMIT;
        END IF;
    END IF;
END$$

DELIMITER ;

