Create PROCEDURE Trans_Save  
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
    IF EXISTS (SELECT * FROM Item_Transaction WHERE Transaction_id = @Transaction_id)
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


create Procedure Transaction_List
  as
  begin
  select * from Item_Transaction
  end

---Delete

create procedure Delete_Transaction
(
@Transaction_id int
)
as
begin
delete from Item_Transaction  where Transaction_id=@Transaction_id
end


---- Edit

 Create Procedure Edit_Transaction  
(
@Transaction_id int
)
  as 
  begin
  select * from Item_Transaction where Transaction_id =@Transaction_id
  end 