



-- Item Master Table
CREATE TABLE Item_master (
    Item_id INT PRIMARY KEY IDENTITY(1,1),
    Item_name VARCHAR(100) NOT NULL,
    Category VARCHAR(50) NOT NULL,
    Rate DECIMAL(10, 2),
    Balance_quantity INT NOT NULL
);

INSERT INTO Item_master (Item_name, Category, Rate, Balance_quantity)
VALUES 
    ('Item 1', 'Electronic', 10.50, 100),
    ('Item 2', 'Consumable', 20.25, 200),
    ('Item 3', 'Furniture', 15.75, 150),
    ('Item 4', 'Stationary', 12.00, 300),
    ('Item 5', 'Stationary', 18.50, 250),
    ('Item 6', 'Electronic', 22.75, 180),
    ('Item 7', 'Stationary', 9.99, 500),
    ('Item 8', 'Consumable', 25.00, 400),
    ('Item 9', 'Furniture', 30.25, 220),
    ('Item 10', 'Stationary', 17.50, 180);

	   select * from Item_master
	   delete from Item_master

-- Department Master Table
CREATE TABLE Department_mast (
    Department_id INT PRIMARY KEY IDENTITY(1,1),
    Department_name VARCHAR(100) NOT NULL
);

INSERT INTO Department_mast (Department_name)
VALUES 
    ('Department 1'),
    ('Department 2'),
    ('Department 3'),
    ('Department 4'),
    ('Department 5'),
    ('Department 6'),
    ('Department 7'),
    ('Department 8'),
    ('Department 9'),
    ('Department 10');

	select * from Department_mast


-- Vendor Master Table
CREATE TABLE Vendor_mast (
    Vendor_id INT PRIMARY KEY IDENTITY(1,1),
    Vendor_name VARCHAR(100) NOT NULL
);

INSERT INTO Vendor_mast (Vendor_name)
VALUES 
    ('Vendor 1'),
    ('Vendor 2'),
    ('Vendor 3'),
    ('Vendor 4'),
    ('Vendor 5'),
    ('Vendor 6'),
    ('Vendor 7'),
    ('Vendor 8'),
    ('Vendor 9'),
    ('Vendor 10');

	select * from Vendor_mast

select * from Vendor_mast

-- Transaction Table
CREATE TABLE Item_Transaction (
    Transaction_id INT PRIMARY KEY IDENTITY(1,1),
    Item_id INT NOT NULL,
    Transaction_date DATETIME NOT NULL,
    Department_id INT,
    Vendor_id INT,
    Quantity INT NOT NULL,
	TransType nvarchar (99)
    FOREIGN KEY (Item_id) REFERENCES Item_master(Item_id),
    FOREIGN KEY (Department_id) REFERENCES Department_mast(Department_id),
    FOREIGN KEY (Vendor_id) REFERENCES Vendor_mast(Vendor_id)
);

INSERT INTO Item_Transaction (Item_id, Transaction_date, Department_id, Vendor_id, Quantity)
VALUES
    (1, '2024-05-08 10:00:00', 2, 1, 5),
    (2, '2024-05-08 11:30:00', 3, 2, 8),
    (3, '2024-05-08 13:45:00', 1, 3, 3),
    (4, '2024-05-08 15:20:00', 2, 2, 6),
    (5, '2024-05-08 16:55:00', 3, 1, 10),
    (1, '2024-05-08 09:15:00', 1, 2, 7),
    (2, '2024-05-08 12:10:00', 2, 3, 4),
    (3, '2024-05-08 14:30:00', 3, 1, 9),
    (4, '2024-05-08 16:00:00', 1, 2, 2),
    (5, '2024-05-08 17:45:00', 2, 3, 5);

select * from Item_Transaction

drop table Item_Transaction

Create table tblCategory(id INT PRIMARY KEY IDENTITY(1,1),Category_Name nvarchar(55))

insert into tblCategory values('Electrinics')
insert into tblCategory values('Consumable')
insert into tblCategory values('Furniture')
insert into tblCategory values('Stationary')
insert into tblCategory values('Electrinics')
 
Select * from tblCategory

-- DDlCategory--

 Create Procedure Ddl_Category
(
  @id int =null
)
as
begin
select * from tblCategory
end



select * from Item_Transaction


CREATE PROCEDURE SaveItem
(
    @Item_id INT,
    @Item_name VARCHAR(99),
    @Category VARCHAR(99),
    @Rate DECIMAL(10, 2),
    @Balance_quantity INT
)
AS
BEGIN
    -- Check if an item with the given ID already exists
    IF EXISTS (SELECT 1 FROM Item_master WHERE Item_id = @Item_id)
    BEGIN
        -- If it exists, update the existing record
        UPDATE Item_master
        SET
            Item_name = @Item_name,
            Category = @Category,
            Rate = @Rate,
            Balance_quantity = @Balance_quantity
        WHERE Item_id = @Item_id
    END
    ELSE
    BEGIN
        -- If it does not exist, insert a new record
        INSERT INTO Item_master
        (Item_name, Category, Rate, Balance_quantity)
        VALUES
        (@Item_name, @Category, @Rate, @Balance_quantity)
    END
END



----Save & Edit

Alter PROCEDURE SaveDepartment
(
    @Department_id INT,
    @Department_name VARCHAR(99)   
)
AS
BEGIN
    -- Check if an item with the given ID already exists
    IF EXISTS (SELECT * FROM Department_mast WHERE Department_id = @Department_id)
    BEGIN
        -- If it exists, update the existing record
        UPDATE Department_mast
        SET
            Department_name = @Department_name
           
        WHERE Department_id = @Department_id
    END
    ELSE
    BEGIN
        -- If it does not exist, insert a new record
        INSERT INTO Department_mast
        (Department_name)
        VALUES
        (@Department_name)
    END
END




--department sp

create Procedure Department_List
  as begin
  select *from Department_mast
  end



  --// delete
  create procedure Delete_Department
(
@Department_id int
)
as
delete from Department_mast where Department_id=@Department_id
return

 

 --Edit  & Fetch Get by Id
 Create Procedure Edit_Department
(
@Department_id int
)
  as begin
  select * from Department_mast where Department_id=@Department_id
  end

 --Items

 create Procedure Item_List
  as begin
  select *from Item_master
  end


  --Delete
    create procedure Delete_Item
(
@Item_id int
)
as
delete from Item_master where Item_id=@Item_id
return



 --Edit  & Fetch Get by Id
 Create Procedure Edit_Item
(
@Item_id int
)
  as
  begin
  select * from Item_master where Item_id=Item_id
  end


  

----Save & Edit

create PROCEDURE Save_Item
(
    @Item_id INT,
    @Item_name VARCHAR(99),
	@Category varchar(99),
    @Rate DECIMAL(10, 2),
    @Balance_quantity INT
)
AS
BEGIN
    -- Check if an item with the given ID already exists
    IF EXISTS (SELECT * FROM Item_master WHERE Item_id = @Item_id)
    BEGIN
        -- If it exists, update the existing record
        UPDATE Item_master
        SET
            Item_name = @Item_name,Category=@Category,Rate=@Rate,Balance_quantity=@Balance_quantity
           
        WHERE Item_id = @Item_id
    END
    ELSE
    BEGIN
        -- If it does not exist, insert a new record
        INSERT INTO Item_master
        (Item_name,Category,Rate,Balance_quantity)
        VALUES
        (@Item_name,@Category,@Rate,@Balance_quantity)
    END
END


--Vendor ----

create Procedure Vendor_List
  as begin
  select * from Vendor_mast
  end


  --Delete
    create procedure Delete_Vendor
(
@Vendor_id int
)
as
delete from Vendor_mast where Vendor_id=@Vendor_id
return


--- Fetxh n Edit

 Create Procedure Edit_Vendor
(
@Vendor_id int
)
  as begin
  select * from Vendor_mast where Vendor_id=@Vendor_id
  end

  --- Save n Edit ---

  
Alter PROCEDURE Save_Vendor
(
    @Vendor_id INT,
    @Vendor_name VARCHAR(99)   
)
AS
BEGIN
 IF EXISTS (SELECT * FROM Vendor_mast WHERE Vendor_name = Vendor_name)
    BEGIN
        -- ItemDescr already exists, do nothing or handle as needed
        PRINT 'Vendor Name already Register, not inserting duplicate value.';
    END
    ELSE
    BEGIN
    -- Check if an item with the given ID already exists
    IF EXISTS (SELECT * FROM Vendor_mast WHERE Vendor_id = @Vendor_id)
    BEGIN
        -- If it exists, update the existing record
        UPDATE Vendor_mast
        SET
            Vendor_name = @Vendor_name
           
        WHERE Vendor_id = @Vendor_id
    END
    ELSE
    BEGIN
        -- If it does not exist, insert a new record
        INSERT INTO Vendor_mast
        (Vendor_name)
        VALUES
        (@Vendor_name)
    END
END
End

----- Transaction ---
--- Department ddl fetch --
 Create Procedure Ddl_Department  
(
  @Department_id int =null
)
as
begin
select * from Department_mast
end


--- Vendor  ddl fetch

 Create Procedure Ddl_Vendor  
(
  @Vendor_id int =null
)
as
begin
select * from Vendor_mast
end



  Alter Procedure [dbo].[ItemQty_NameFetch]    
 (  
 @Item_id int=null  
 )   
as    
begin    
select * from Item_master 
end    


ALTER Procedure [dbo].[ddlItemQty_Fetch]
 (
 @Item_id int=null
 ) 
as  
begin  
select * from Item_master  where Item_id=@Item_id
end 

-- save n udate 

Create PROCEDURE Transaction_Save  
(    
    @Transaction_id INT,      
    @Item_id INT,        
    @Transaction_date DATETIME,          
    @Department_id INT,          
    @Vendor_id INT,          
    @Quantity INT,
    @ItemQtyTotal INT,
    @TransType NVARCHAR(55),
    @Balance_quantity INT                               
)    
AS 
BEGIN
    IF EXISTS (SELECT * FROM   WHERE Transaction_id = @Transaction_id)
    BEGIN
        UPDATE Item_Transaction 
        SET Item_id = @Item_id,
            TransType = @TransType,
            Quantity = @Quantity,
            Transaction_date = @Transaction_date 
        WHERE Transaction_id = @Transaction_id;
    END
    ELSE     
    BEGIN
        INSERT INTO Item_Transaction (Item_id, Transaction_date, Department_id, Vendor_id, Quantity, TransType)    
        VALUES (@Item_id, @Transaction_date, @Department_id, @Vendor_id, @Quantity, @TransType);

        UPDATE Item_master 
        SET Balance_quantity = @ItemQtyTotal 
        WHERE Item_id = @Item_id;  
    END  
END

----List

create Procedure Transaction_List
  as begin
  select *from Item_Transaction
  end

---Delete

    create procedure Delete_Transaction
(
@Transaction_id int
)
as
delete from Item_Transaction  where Transaction_id=@Transaction_id
return


---- Edit

 Create Procedure Edit_Transaction
(
@Transaction_id int
)
  as begin
  select * from Item_Transaction where Transaction_id =@Transaction_id
  end